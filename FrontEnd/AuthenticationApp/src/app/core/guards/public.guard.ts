import { CanActivateFn, Router } from "@angular/router";
import { inject } from "@angular/core";
import { SessionService } from "../../features/auth/pages/login/services/session.service";

export const publicGuard: CanActivateFn = () => {

  const session = inject(SessionService);
  const router = inject(Router);

  if (!session.isAuthenticated()) return true;

  return router.createUrlTree(['/dashboard']);
};
