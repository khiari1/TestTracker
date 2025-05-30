import { IModelBase } from '../shared/model-base.interface';

export class Job implements IModelBase {
  id: number | undefined;
  Name: string | undefined;
  TaskId: number | undefined;
  Description: string | undefined;
  Startdate: Date | undefined;
  Finishdate: Date | undefined;
  JobState: JobEnum | undefined;
}
enum JobEnum {
  Completed,
  Inprogress,
  Canceled,
  Failed,
}
