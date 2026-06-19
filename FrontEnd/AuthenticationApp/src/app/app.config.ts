import {
  provideAppInitializer,
  ApplicationConfig,
  inject,
  provideBrowserGlobalErrorListeners,
} from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { authInterceptor } from './core/http/auth.interceptor';
import { firstValueFrom } from 'rxjs';
import { TokenService } from './features/auth/pages/login/services/token.service';
import { SessionService } from './features/auth/pages/login/services/session.service';
import { AuthService } from './features/auth/pages/login/services/auth.service';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideRouter(routes),
    // provideClientHydration(withEventReplay()),
    provideHttpClient(withFetch(), withInterceptors([authInterceptor])),

    provideAppInitializer(async () => {
      const tokenService = inject(TokenService);
      const sessionService = inject(SessionService);
      const authService = inject(AuthService);

      const token = tokenService.getToken();
      if (!token) return;

      try {
        const user = await firstValueFrom(authService.getMe());
        sessionService.setUser(user);
      } catch {
        tokenService.clear();
      }
    }),
  ],
};
