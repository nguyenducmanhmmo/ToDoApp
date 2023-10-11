import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/shared/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'todo-angular';
  get userToken() {
    return this.authService.getLoggedInUser();
  }

  constructor(private authService: AuthService, private router: Router) {  }


  logout() {
    this.authService.logout();
    this.router.navigate(['../../public/login']);
    window.location.reload();
  }

}