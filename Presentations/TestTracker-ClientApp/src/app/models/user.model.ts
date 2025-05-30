import { GroupModel } from './group.model';

export class StoreUser {
  login: string | undefined;
  firstName: string | undefined;
  lastName: string | undefined;
  token: string | undefined;

}
export class UserModel {
  id: number | undefined;
  firstName: string | undefined;
  lastName: string | undefined;
  name : string | undefined;
  email : string | undefined;
  accountEnabled :boolean | undefined;
  photo : string | undefined;
  objectId : string | undefined;
  groups: GroupModel[] = [];
}
export class UserInfo {
  id: number | undefined;
  firstName: string | undefined;
  groups: GroupModel[] = [];
}
export class UserLoginModel {
  login: string | undefined;
  password: string | undefined;
}
export class UserPassModel {
  password?: string | null;

  newpassword?: string | null;

  confirmpassword?: string | null;
}

export class UserImageModel {
  userImage: string | undefined;
}

export interface UserAzureAdModel {
  id: string;
  userName: string;
  firstName: string;
  userPricipalName:string;
  lastName: string;
  login: string | null;
  password: string | null;
  email: string | null;
  phoneNumber: string | null;
  accountEnabled: boolean | null;
  city: string | null;
  companyName: string | null;
  country: string | null;
  departement: string | null;
  employeeType: string | null;
  jobTitle: string | null;
  mail: string | null;
  mobilePhone: string | null;
  officeLocation: string | null;
  postalCode: string | null;
  streetAddress: string | null;
  mailNickname: string;
  givenName: string;
  creationType : string | null;
  isAdmin : boolean ;
}

