import { inject, Injectable, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

@Injectable({ providedIn: 'root' })
export class TokenService {

  private readonly KEY = 'access_token';

  private readonly platformId = inject(PLATFORM_ID);

  private get isBrowser(): boolean {
    return isPlatformBrowser(this.platformId);
  }

  getToken(): string | null {
    if (!this.isBrowser) return null;

    return localStorage.getItem(this.KEY);
  }

  setToken(token: string): void {
    if (!this.isBrowser) return;

    localStorage.setItem(this.KEY, token);
  }

  clear(): void {
    if (!this.isBrowser) return;

    localStorage.removeItem(this.KEY);
  }
}
