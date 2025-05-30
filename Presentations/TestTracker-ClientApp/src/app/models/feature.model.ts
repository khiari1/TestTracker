import { IModelBase } from "../shared/model-base.interface";

export class Feature implements IModelBase {
  id: number | undefined;
  name: string | undefined;
  description: string | undefined;
}
