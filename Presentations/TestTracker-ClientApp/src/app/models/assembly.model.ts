import { IModelBase } from '../shared/model-base.interface';

export class Assembly implements IModelBase {
  id: number | undefined;
  keyGroupAssemblyBytes: File | undefined;
}
