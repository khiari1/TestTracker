import { Inject, Injectable } from '@angular/core';
import {
  CanActivateChild,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  UrlTree,
  Router,
  NavigationExtras,
  CanActivate,
} from '@angular/router';
import { Observable, map } from 'rxjs';
import { Rights } from './rights';
import { UserService } from '../services/user.service';

@Injectable({
  providedIn: 'root',
})
export class PermissionGuard implements CanActivateChild, CanActivate {
  constructor(
    private router: Router,
    private userService: UserService
  ) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {


    const requiredPermission: Rights = route.data['requiredPermission'];
    const hasPermissions = this.userService.hasPermission(requiredPermission) || this.userService.IsAdmin();
    if (hasPermissions) {
      return true;
    } else {
      sessionStorage.setItem('previousUrl', state.url);
      console.log('state.url', state.url);

      const settingsRegex = /^\/dashboard\/settings(?:\/|$)/;
      const navigationParts = state.url
        .split('/')
        .filter((part) => part !== '');
      const navigationName = navigationParts[navigationParts.length - 1];

      if (settingsRegex.test(state.url)) {
        this.router.navigate(['/dashboard/settings/unauthorized'], {
          queryParams: { navigationName },
        });
      } else {
        this.router.navigate(['/dashboard/unauthorized'], {
          queryParams: { navigationName },
        });
      }

      return false;
    }
  }

  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ):
    | Observable<boolean | UrlTree>
    | Promise<boolean | UrlTree>
    | boolean
    | UrlTree {

    const requiredPermission: Rights = childRoute.data['requiredPermission'];

    const hasPermissions = this.userService.hasPermission(requiredPermission) || this.userService.IsAdmin();
    if (hasPermissions) {
      return true;
    } else {
      sessionStorage.setItem('previousUrl', state.url);
      console.log('state.url', state.url);

      const settingsRegex = /^\/dashboard\/settings(?:\/|$)/;

      if (settingsRegex.test(state.url)) {
        this.router.navigate(['/dashboard/settings/unauthorized']);
      } else {
        this.router.navigate(['/dashboard/unauthorized']);
      }

      return false;
    }
  }
}
