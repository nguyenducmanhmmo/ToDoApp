import { Injectable } from "@angular/core";
import { Observable, tap } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment.prod";
import { User } from "./user.model";
import {JwtHelperService} from "@auth0/angular-jwt";
import { LOCALSTORAGE_KEY_TODO_APP } from "./consant";

@Injectable({
    providedIn: 'root'
  })
  export class AuthService {
    readonly APIUrl = environment.apiUrl;
    constructor(private http: HttpClient,private jwtService: JwtHelperService) { }
    login(user: User): Observable<any> {
        return this.http.post( this.APIUrl + '/auth/login', user).pipe(
            tap((res : any) => localStorage.setItem(LOCALSTORAGE_KEY_TODO_APP, res.token)),
          );
      }
    
    getLoggedInUser() {
        return localStorage.getItem(LOCALSTORAGE_KEY_TODO_APP);
    }

    logout() {
        localStorage.removeItem(LOCALSTORAGE_KEY_TODO_APP);
    }
  }