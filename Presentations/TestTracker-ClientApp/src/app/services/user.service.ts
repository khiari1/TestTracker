import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { UserAzureAdModel, UserModel } from '../models/user.model';
import { HttpService } from '../shared/http-service.service';
import { Observable, firstValueFrom } from 'rxjs';
import { Permission } from '../models/permission.model';
import { MSAL_INSTANCE } from '@azure/msal-angular';
import { IPublicClientApplication } from '@azure/msal-browser';

@Injectable({
  providedIn: 'root',
})
export class UserService extends HttpService<UserAzureAdModel> {
  /**
   *
   */
  permissions: Permission[] = [];
  currentUser: UserAzureAdModel | undefined;

  constructor(
    protected override httpClient: HttpClient,
    @Inject(MSAL_INSTANCE) private msalInstance: IPublicClientApplication
  ) {
    super(httpClient, environment.baseUrl, environment.user);
  }

  hasPermission(key: string): boolean {
    return (this.permissions.find((p) => p.key === key) != null) || this.IsAdmin();
  }
  IsAdmin(): boolean {
    if (this.currentUser) {
      return this.currentUser.isAdmin;
    } else {
      return false;
    }
  }

  getCurrentUser(): Observable<UserModel[]> {
    return this.httpClient.get<UserModel[]>(
      `${environment.baseUrl}/${environment.user}/me`
    );
  }

  synchronizeUserFromAzureAD(): Observable<Object> {
    return this.httpClient.get(
      `${environment.baseUrl}/${environment.user}/SyncStore`
    );
  }

  me(): Observable<any> {
    return this.httpClient.get<any>(
      `${environment.baseUrl}/${environment.user}/me`
    );
  }
  UpdateUser(id: number): Observable<UserModel[]> {
    return this.httpClient.put<any>(
      `${environment.baseUrl}/${environment.user}/modify_user`,
      id
    );
  }
  mePhoto(): Observable<Blob> {
    return this.httpClient.get(
      `${environment.baseUrl}/${environment.user}/me/photo`,
      { responseType: 'blob' }
    );
  }
  InviteUser(
    invitedUserEmailAddress: string,
    firstName: string,
    lastName: string,
    displayName: string,
    sendMessage: boolean
  ) {
    return this.httpClient.post<any>(
      `${environment.baseUrl}/${environment.user}/InviteUser`,
      {
        invitedUserEmailAddress: invitedUserEmailAddress,
        InviteRedirectUrl: ' ',
        firstName: firstName,
        lastName: lastName,
        displayName: displayName,
        sendMessage: sendMessage,
      }
    );
  }

  public async permissionFactory() {
    try {
      if (this.msalInstance.getActiveAccount() != null) {
        console.log(this.msalInstance.getActiveAccount());
        this.permissions = await firstValueFrom(
          this.httpClient.get<Permission[]>(
            `${environment.baseUrl}/${environment.Permission}/alluserpermission`
          )
        );
        this.currentUser = await firstValueFrom(
          this.httpClient.get<any>(
            `${environment.baseUrl}/${environment.user}/me`
          )
        );
        
      }
      
    } catch {}
  }
}
