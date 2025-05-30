import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { firstValueFrom } from "rxjs";
import { environment } from "src/environments/environment.prod";
import { UserLoginModel } from "../models/user.model";
import { StoreUser} from "../models/user.model";
import { StorageService } from "./storage.service";

export class AuthenticationBaseService {
    /**
   * Create a point.
   * @param {HttpClient} httpClient - injecting The httpClient for performing http request.
   * @param {string} endpoint - The endpoint URL.
   */
    constructor(protected _httpClient: HttpClient, protected storageService: StorageService, public _endpoint: string) { }


    public async login(user: UserLoginModel): Promise<LoginResponse> {

        let loginResponse: LoginResponse = new LoginResponse();
        console.log(environment["baseUrl"]);
        try {
            let result = await firstValueFrom(this._httpClient.post<StoreUser>(this._endpoint, user));
            this.storageService.setItem("userLogin", result);
            return new LoginResponse(result, "", "Authentication with succesfull", true);
        } catch (ex) {
            if (ex instanceof (HttpErrorResponse)) {
                return new LoginResponse(undefined, ex.error, ex.message, ex.ok);
            }
        }
        return loginResponse;
    }

    public getCurrentUser(): StoreUser | null {
        if (this.isAuthenticated()) {
            let user = this.storageService.getItem<StoreUser>('userLogin');
            return user;
        }
        return null;
    }

    public isAuthenticated(): boolean {
        var user = this.storageService.getItem<StoreUser>('userLogin');
        return user !== null
    }

    public logOut() {
        this.storageService.removeItem('userLogin');
    }
}

export class LoginResponse {
    /**
     *
     */
    constructor(data?: StoreUser, error?: string, message?: string, ok?: boolean) { 
        this.data = data;
        this.error = error;
        this.message = message;
        this.ok = ok??true;
    }

    data?: StoreUser;
    error?: string | undefined;
    message?: string | undefined;
    ok: boolean = true;
}
