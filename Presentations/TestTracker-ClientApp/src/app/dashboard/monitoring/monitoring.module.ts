import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { InputTextModule } from 'primeng/inputtext';
import { MessagesModule } from 'primeng/messages';
import { MultiSelectModule } from 'primeng/multiselect';
import { SidebarModule } from 'primeng/sidebar';
import { TableModule } from 'primeng/table';
import { TabMenuModule } from 'primeng/tabmenu';
import { TagModule } from 'primeng/tag';
import { AttachementModule } from 'src/app/tsing/attachement/attachement.module';
import { TabViewModule } from 'primeng/tabview';
import { EditorModule } from 'primeng/editor';
import { CommentModule } from 'src/app/tsing/Comment/comment.module';
import { SelectButtonModule } from 'primeng/selectbutton';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient } from '@angular/common/http';
import { MenuModule } from 'primeng/menu';
import { MonitoringDetailsComponent } from './monitoring-details.component';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { HttpLoaderFactory } from 'src/app/shared/shared.module';
import { MonitoringRoutingModule } from './monitoring-routing.module';
import { MonitoringComponent } from './monitoring.component';
import { AllMonitoringComponent } from './all-monitoring.component';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { BadgeModule } from 'primeng/badge';
import { MonitoringFormComponent } from './monitoring-form.component';
import { ContextMenuModule } from 'primeng/contextmenu';
import { SpinnerModule } from 'src/app/tsing/spinner/spinner.module';
import { CardModule } from 'primeng/card';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { MessageModule } from "primeng/message";
import { ChipModule } from "primeng/chip";
import { InputNumberModule } from 'primeng/inputnumber';
import { ChartModule } from 'primeng/chart';
import { AccordionModule } from 'primeng/accordion';
import { InplaceModule } from 'primeng/inplace';
import { SharedModule } from '../SharedModule.module';
import { StateIconModule } from 'src/app/tsing/state-icon/state-icon.module';
import { PageHeaderbModule } from 'src/app/tsing/page-header/pageHeader.module';
import { CreateJobComponent } from './create-job.component';
import { PanelModule } from 'primeng/panel';
import { MonitoringDetailForm } from './monitoring-detail-form.component';
import { ViewsModule } from 'src/app/tsing/views/views.module';
import { AvatarModule } from 'src/app/tsing/avatar/avatar.module';
import { MenuBarModule } from 'src/app/tsing/menu-bar/menu-bar.module';

export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
@NgModule({
  declarations: [
    MonitoringComponent,
    MonitoringDetailsComponent,
    AllMonitoringComponent,
    MonitoringFormComponent,
    CreateJobComponent,
    MonitoringDetailForm
  ],
  imports: [
    ViewsModule,
    BrowserModule,
    CommonModule,
    FormsModule,
    TableModule,
    ButtonModule,
    TabMenuModule,
    InputTextModule,
    DropdownModule,
    MessagesModule,
    MultiSelectModule,
    TagModule,
    CalendarModule,
    SidebarModule,
    TabViewModule,
    CommentModule,
    AttachementModule,
    EditorModule,
    SelectButtonModule,
    MenuModule,
    MenuBarModule,
    MonitoringRoutingModule,
    ConfirmPopupModule,
    BadgeModule,
    ContextMenuModule,
    SpinnerModule,
    MessageModule,
    ChipModule,
    ProgressSpinnerModule,
    ChartModule,
    CardModule,
    AccordionModule,
    InplaceModule,
    StateIconModule,
    [SharedModule],
    InputNumberModule,
    PageHeaderbModule,
    PanelModule,
    AvatarModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
  ],
})
export class MonitoringModule {}
