import { IModelBase } from '../shared/model-base.interface';

export interface Attachement {
  id: number;
  folder: string;
  objectId: number | null;
  fileName: string;
  extension: string;
  fileSize: number;
  data: string;
  date: string; 
  userId: string | null;
}
