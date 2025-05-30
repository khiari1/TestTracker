import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { AuthenticationService } from "../services/authentication.service";

@Injectable()
export class AuthGuardService implements CanActivate {

    constructor(private _router: Router,
        private _authenticationService: AuthenticationService) {
    }

    canActivate(route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot,): boolean {

        //check some condition  
        if (!this._authenticationService.isAuthenticated() && state.url.includes('/dashboard')) {
            this._router.navigate(["/login"])
            //redirect to login/home page etc
            //return false to cancel the navigation
            return false;
        } else if (this._authenticationService.isAuthenticated() && state.url.includes('/login')) {
            this._router.navigate(["/dashboard"])
            return false;
        }
        return true;
    }

}