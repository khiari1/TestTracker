import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EnqueuedJobComponent } from './enqueued-job.component';
import { FailedJobComponent } from './failed-job.component';
import { ProcessingJobComponent } from './processing-job.component';
import { RecurringJobComponent } from './recurring-job.component';
import { ScheduleJobComponent } from './schedule-job.component';
import { SucceededJobComponent } from './succeeded-job.component';
const routes: Routes = [
  { path: '', redirectTo: 'reccuring', pathMatch: 'full' },
  { path: 'reccuring', component : RecurringJobComponent },
  { path: 'enqueued', component : EnqueuedJobComponent },
  { path: 'failed', component : FailedJobComponent },
  { path: 'processing', component : ProcessingJobComponent },
  
  { path: 'schedule', component : ScheduleJobComponent },
  { path: 'succeeded', component : SucceededJobComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class jobRoutingModule {}
