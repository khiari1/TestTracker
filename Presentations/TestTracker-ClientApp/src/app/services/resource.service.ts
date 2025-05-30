import { HttpClient } from '@angular/common/http';
import { Injectable, KeyValueDiffer } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { Observable } from 'rxjs';
import { IKeyValuePair } from '../models/Ikey-value-pair.interface';
import { UserAzureAdModel, UserInfo, UserModel } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class ResourceService {
  private _module: string = 'GetModules';
  private _user: string = 'GetAllUsers';
  constructor(private http: HttpClient) {}

  getModules(
    searchPattern?: string
  ): Observable<IKeyValuePair<number, string>[]> {
    return this.getDispatcher(this._module, searchPattern);
  }


  getAllUsers(): Observable<IKeyValuePair<string, string>[]> {
    return this.http.get<IKeyValuePair<string, string>[]>(
      `${environment.baseUrl}/${environment.resourceEndpoint}/Users/${this._user}`
    );
  }

  getAllUsersById(
    userId: number): Observable<IKeyValuePair<number, string>[]> {
    return this.http.get<IKeyValuePair<number, string>[]>(
      `${environment.baseUrl}/${environment.resourceEndpoint}/Users/${userId}`
    );
  }
  getUserById(userId: number): Observable<UserAzureAdModel[]> {
    return this.http.get<UserAzureAdModel[]>(
      `${environment.baseUrl}/${environment.resourceEndpoint}/Users/${userId}`
    );
  }



  getUsers(searchPattern?: string): Observable<UserModel[]> {
    if (searchPattern === undefined) {
      return this.http.get<UserModel[]>(
        `${environment.baseUrl}/${environment.resourceEndpoint}/Users`
      );
    }
    return this.http.get<UserModel[]>(
      `${environment.baseUrl}/${environment.resourceEndpoint}/Users/${searchPattern}`
    );
  }

  private getDispatcher(segment: string, searchPattern?: string) {
    if (searchPattern === undefined) {
      return this.http.get<IKeyValuePair<number, string>[]>(
        `${environment.baseUrl}/${environment.resourceEndpoint}/${segment}`
      );
    }
    return this.http.get<IKeyValuePair<number, string>[]>(
      `${environment.baseUrl}/${environment.resourceEndpoint}/${segment}/${searchPattern}`
    );
  }
}
