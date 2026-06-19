import { computed, Injectable, signal } from "@angular/core";
import { User } from "../../../models/user.model";

@Injectable({ providedIn: 'root' })
export class SessionService {

  private readonly _user = signal<User | null>(null);

  readonly user = computed(() => this._user());
  readonly isAuthenticated = computed(() => !!this._user());

  setUser(user: User) {
    this._user.set(user);
  }

  clear() {
    this._user.set(null);
  }
}
