import { IModelBase } from "../shared/model-base.interface";

export class Label implements IModelBase {
  id: number | undefined;
  name: string | undefined;
  description: string | undefined;
  color: string | undefined;
}
