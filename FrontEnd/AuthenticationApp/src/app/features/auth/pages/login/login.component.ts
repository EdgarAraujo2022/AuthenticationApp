import { CommonModule } from "@angular/common";
import { Component, inject, signal } from "@angular/core";
import { FormBuilder, ReactiveFormsModule, Validators } from "@angular/forms";
import { AuthService } from "./services/auth.service";
import { TokenService } from "./services/token.service";
import { SessionService } from "./services/session.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  private readonly fb = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  private readonly tokenService = inject(TokenService);
  private readonly sessionService = inject(SessionService);
  private readonly router = inject(Router);

  isLoading: boolean = false;
  error : string = ''

  form = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]],
    clientId: ['Auth'],
    clientSecret: ['Auth_App'],
  });

  submit() {
    if (this.form.invalid) return;

    this.isLoading = true;
    this.error = '';

    this.authService.login(this.form.getRawValue())
      .subscribe({
        next: (response) => {
          this.tokenService.setToken(response.accessToken);
          this.sessionService.setUser(response.user);
          this.router.navigate(['/dashboard']);
        },
        error: () => {
          this.error = 'Usuário ou senha inválidos'; 
          this.isLoading = false;
        }
      });
  }
}
