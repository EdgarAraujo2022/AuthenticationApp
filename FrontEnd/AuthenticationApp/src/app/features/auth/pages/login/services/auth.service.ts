import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { AuthResponse } from "../../../models/auth-response.model";
import { LoginRequest } from "../../../models/login-request.model";
import { User } from "../../../models/user.model";


@Injectable({ providedIn: 'root' })
export class AuthService {

  private readonly http = inject(HttpClient);

  login(data: LoginRequest) {
    return this.http.post<AuthResponse>(
      'http://localhost:5114/api/auth/login',
      data
    );
  }

  getMe() {
    return this.http.get<User>(
      '/api/auth/me'
    );
  }
}
