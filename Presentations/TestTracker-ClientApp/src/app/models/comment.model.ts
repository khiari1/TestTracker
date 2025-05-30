import { IModelBase } from "../shared/model-base.interface";
import { UserInfo, UserModel } from "./user.model";


export class Comment implements IModelBase{
  id: number | undefined;
  keyGroup: string | undefined;
  objectId : number | undefined;
  content: string = '';
  date : Date|undefined ;
  UserId: number | undefined;
  isCurrentUser:boolean | undefined;
}
