import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Router} from "@angular/router";
import {tap} from "rxjs";
import { AuthService } from 'src/shared/auth.service';
import { User } from 'src/shared/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  form: FormGroup = new FormGroup({
    email: new FormControl(null, [Validators.required]),
    password: new FormControl(null, [Validators.required])
  });

  constructor(private authService: AuthService, private router: Router) { }

  login() {
    const user : User = { Username : this.email.value, Password: this.password.value}
    if (this.form.valid) {
      this.authService.login(user).pipe(
        tap(() => this.router.navigate(['../../private/todo']))
      ).subscribe();
    }
  }


  get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  get password(): FormControl {
    return this.form.get('password') as FormControl;
  }

}