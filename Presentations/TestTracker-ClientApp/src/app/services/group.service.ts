import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { Group } from '../models/group.model';
import { HttpService } from '../shared/http-service.service';
import { PermissionModel } from '../models/permission.model';
import { UserAzureAdModel, UserModel } from '../models/user.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GroupService extends HttpService<Group> {
  /**
   *
   */
  constructor(protected override httpClient: HttpClient) {
    super(httpClient, environment.baseUrl, environment.Group);
  }

  updatePermission(groupId: string, permission: string[]): Observable<any> {
    return this.httpClient.put<any>(
      `${this.baseUrl}/${this.endpoints}/${groupId}/Permissions`,
     permission
    );
  }

  DeletePermissions(groupId: string, permissions: string[]): Observable<any> {
    const url = `${this.baseUrl}/${this.endpoints}/${groupId}/DeletePermissions`;

    return this.httpClient.request<any>('delete', url, { body: permissions });
  }
  addUser(groupId: string, userId: string) {
    return this.httpClient.put<any>(
      `${this.baseUrl}/${this.endpoints}/AddUser`,
      { groupId: groupId, userId: userId }
    );
  }
  getPermissions(groupId: string) {
    return this.httpClient.get<any>(
      `${this.baseUrl}/${this.endpoints}/${groupId}/Permissions`
    );
  }

  getAllGroups(): Observable<any> {
    return this.httpClient.get<any>(`${this.baseUrl}/${this.endpoints}`);
  }
  getGroupById(id: string): Observable<any> {
    if (id === null || id === undefined)
      throw new Error("The parameter 'id' cannot be null.");
    return this.httpClient.get<any>(`${this.baseUrl}/${this.endpoints}/${id}`);
  }
  createGroup(group: Group) {
    return this.httpClient.post<Group>(
      `${this.baseUrl}/${this.endpoints}`,
      group
    );
  }
  removeUser(groupId: string, userId: string) {
    return this.httpClient.put<any>(
      `${this.baseUrl}/${this.endpoints}/RemoveUser`,
      { groupId: groupId, userId: userId }
    );
  }
  getUserNotInGroup(groupId: string): Observable<UserAzureAdModel[]> {
    return this.httpClient.get<UserAzureAdModel[]>(
      `${this.baseUrl}/${this.endpoints}/GetUserNotInGroup/${groupId}`
    );
  }

  getUserInGroup(groupId: string): Observable<UserModel[]> {
    return this.httpClient.get<UserModel[]>(
      `${this.baseUrl}/${this.endpoints}/GetUsersInGroup/${groupId}`
    );
  }
}
