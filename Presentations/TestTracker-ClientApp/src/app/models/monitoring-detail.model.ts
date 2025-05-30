import { IModelBase } from "../shared/model-base.interface";

export interface MonitoringDetail {
  id: number;
  date: Date;
  status: number;
  message: string | null;
  stackTrace: string | null;
  ticket: string | null;
  buildVersion: string | null;
  duration: string;
}

export class MonitoringDetailsChart implements IModelBase {
  id: number | undefined;
  date: string | undefined;
  moduleName : string | undefined;
  errorMessage: string | undefined;
  testResult: string | undefined;
  get state(): string {
    return this.testResult === '1' ? 'error' : 'success';
  }
}

export class MonitoringMonitoringDetails implements IModelBase {
  id: number | undefined;
  date!: string ;
  testResult : string | undefined;
  errorMesage : string | undefined;
  stackTrace : string | undefined;
  duration! : string
  ticket : string | undefined;
  buildVersion : string | undefined;
  nameMethodeTest! : string;
  description?: string;
  useCase : string | undefined;
  preconditions : string | undefined;
  awaitedResult : string | undefined;
  failingSince : Date | undefined;
  moduleName : string | undefined;
  testerName : string | undefined;
  state! : number
}
