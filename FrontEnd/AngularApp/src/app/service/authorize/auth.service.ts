import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {LoginRequest} from "../../models/login/login-request";
import {map, Observable} from "rxjs";
import {LoginResponse} from "../../models/login/login-response";
import {UserService} from "../userService/user.service";
import {Router} from "@angular/router";


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpClient: HttpClient, private router: Router, private userService: UserService) { }

  login(credential: LoginRequest) : Observable<LoginResponse>{
    return this.httpClient.post<LoginResponse>("http://localhost:5117/login", credential)
      .pipe(map(response => {
        localStorage.setItem('accessToken', response.accessToken);
        document.cookie = `refreshToken=${response.refreshToken};`;
        return response;
      }));
  }

  refreshToken(): Observable<LoginResponse>{
    const refreshToken = this.getRefreshTokenFromCookie();
    return this.httpClient.post<LoginResponse>("http://localhost:5117/refresh", { refreshToken})
      .pipe(map(response => {
        localStorage.setItem('accessToken', response.accessToken);
        document.cookie = `refreshToken=${response.refreshToken};`;
        return response;
      }))
  }

  private getRefreshTokenFromCookie(): string | null {
    const cookieString = document.cookie;
    const cookieArray = cookieString.split('; ');

    for (const cookie of cookieArray){
      const [name, value] = cookie.split('=');
      if (name == 'refreshToken'){
        return value;
      }
    }
    return null;
  }

  logout(){
    localStorage.removeItem('accessToken')
    this.userService.logoutUser();
    this.router.navigate(['/home-page'])
  }
  isLoggedIn() : boolean{
    return localStorage.getItem('accessToken') !== null;
  }
}
