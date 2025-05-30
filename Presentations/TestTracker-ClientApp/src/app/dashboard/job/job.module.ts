import { NgModule } from "@angular/core";
import { EnqueuedJobComponent } from "./enqueued-job.component";
import { FailedJobComponent } from "./failed-job.component";
import { ProcessingJobComponent } from "./processing-job.component";
import { RecurringJobComponent } from "./recurring-job.component";
import { ScheduleJobComponent } from "./schedule-job.component";
import { SucceededJobComponent } from "./succeeded-job.component";
import { BrowserModule } from "@angular/platform-browser";
import { CommentModule } from "src/app/tsing/Comment/comment.module";
import { PageHeaderbModule } from "src/app/tsing/page-header/pageHeader.module";
import { jobRoutingModule } from "./job-routing.module";
import { BreadcrumbModule } from "primeng/breadcrumb";
import { MenubarModule } from "primeng/menubar";
import { JobComponent } from "./job.component";
import { TableModule } from "primeng/table";
import { ButtonModule } from "primeng/button";
import { GridModule } from "src/app/tsing/grid/grid.module";
import { MenuModule } from "primeng/menu";
import { ChipModule } from "primeng/chip";
import { TagModule } from "primeng/tag";
import { ViewsModule } from "src/app/tsing/views/views.module";
import { TranslateModule } from "@ngx-translate/core";
import { MenuNavBarModule } from "src/app/tsing/menu-nav-bar/menu-nav-bar.module";

@NgModule
({
  declarations : [
    EnqueuedJobComponent,
    FailedJobComponent,
    ProcessingJobComponent,
    RecurringJobComponent,
    ScheduleJobComponent,
    SucceededJobComponent,
    JobComponent
  ],
  imports : [
    BrowserModule,
    CommentModule,
    PageHeaderbModule,
    jobRoutingModule,
    MenubarModule,BreadcrumbModule,
    TableModule,
    ButtonModule,
    GridModule,
    MenuModule,
    ChipModule,
    TagModule,
    ViewsModule,
    TranslateModule,
    MenuNavBarModule
  ]
})
export class JobModule {

}
