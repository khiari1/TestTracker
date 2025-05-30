import { IModelBase } from '../shared/model-base.interface';
import {
  MonitoringDetail,
  MonitoringMonitoringDetails,
} from './monitoring-detail.model';

export class Monitoring implements IModelBase {
  id!: number;
  nameMethodTest: string | undefined;
  useCase: string | undefined;
  preconditions: string | undefined;
  awaitedResult: string | undefined;
  failingSince: Date | undefined;
  moduleName: string | undefined;
  menuName: string | undefined;
  testerName: string | undefined;
  testerId: string | undefined;
  responsibleId : string | undefined;
  responsibleName : string | undefined;
  monitoringDetails: MonitoringDetail[] = [];
  taskState : number | undefined;
  moduleId: number | undefined;
  comment: string | undefined;
}
export class monitoringDetail implements IModelBase {
  id: number | undefined;
  nameMethodeTest: string | undefined;
  useCase: string | undefined;
  preconditions: string | undefined;
  awaitedResult: string | undefined;
  failingSince: Date | undefined;
  moduleName: string | undefined;
  buildVersion: string | undefined;
  testResult: string | undefined;
  errorMesage: string | undefined;
  date: Date | undefined;

}

export class ModuleSummary {
  id: number | undefined;
  name: string | undefined;
  totalError: number | undefined;
  totalSuccess: number | undefined;
  totalWarning: number | undefined;
  details: MonitoringMonitoringDetails[] = [];
}
