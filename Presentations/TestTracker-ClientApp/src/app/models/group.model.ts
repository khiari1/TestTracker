import { IModelBase } from '../shared/model-base.interface';
import { PermissionModel } from './permission.model';
import {  UserModel } from './user.model';

export class Group {
  id: string | undefined;
  name: string | undefined;
  description: string | undefined;
  userGroups: UserModel[] = [];
  permissions: PermissionModel[] = [];
}

export class GroupModel {
  id: string | undefined;
  name: string | undefined;
  description: string | undefined;
  permissions: PermissionModel[] = [];
}



