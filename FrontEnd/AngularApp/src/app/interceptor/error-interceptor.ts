import {Injectable} from "@angular/core";
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {AuthService} from "../service/authorize/auth.service";
import {catchError, Observable, switchMap, throwError} from "rxjs";
import {throws} from "node:assert";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor{
  constructor(private authService: AuthService) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError(error => {
        if(error instanceof HttpErrorResponse && error.status === 401){
          if(!request.url.includes('/login')){
            return this.handle401Error(request, next);
          }
          else {
            return throwError(() => error);
          }
        }
        else {
          return throwError(() => error);
        }
      })
    );
  }

  private addToken(request: HttpRequest<any>): HttpRequest<any> {
    const accessToken = localStorage.getItem('accessToken');
    if(accessToken){
      return request.clone({
        setHeaders: {
          Authorization: `Bearer ${accessToken}`
        }
      });
    }
    return request;
  }

  private handle401Error(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>>{
    return this.authService.refreshToken().pipe(
      switchMap(() => {
        return next.handle(this.addToken(request))
      }),
      catchError((error) => {
        console.error("Failed to refresh token", error)
        this.authService.logout();
        return throwError(() => error)
      })
    )
  }
}
