import { IModelBase } from "../shared/model-base.interface";

export class GeneralSettings implements IModelBase {
   id: number | undefined;
  organisationId: number | undefined;
  pat: string | undefined;
  projectId: number | undefined;
  projectName: string | undefined;
  TeamId: number | undefined;
  teamName: string | undefined;

}

export class AzureDevopsSettings implements IModelBase {
  id: number | undefined;
  organisationId: number | undefined;
  pat: string | undefined;


}
export class Project implements IModelBase {
  id: number | undefined;
  projectId: number | undefined;
  projectName: string | undefined;


}
export class Team implements IModelBase {
  id: number | undefined;
  teamId: number | undefined;
  teamName: string | undefined;


}
