import {
  HttpClient,
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Token } from '@angular/compiler';
import { Injectable, Injector } from '@angular/core';
import { EMPTY, Observable } from 'rxjs';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root',
})
export class RequestInterceptorService implements HttpInterceptor {
  constructor(private inject: Injector) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    var authService = this.inject.get(AuthenticationService);

    const Token = authService.getCurrentUser()?.token;

    if (authService.isAuthenticated() && Token) {
      const cloned = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + Token),
      });
      return next.handle(cloned);
    } else {
      return next.handle(req);
    }
  }
}
