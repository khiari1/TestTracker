import {  NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BoardLayoutModule } from './dashboard-layout/dashboard-layout.module';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DashboardComponent } from './dashboard.component';
import {FileUploadModule} from 'primeng/fileupload';
import { TreeTableModule } from 'primeng/treetable';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';
import { InputTextModule } from 'primeng/inputtext';
import { ToastModule } from 'primeng/toast';
import { ContextMenuModule } from 'primeng/contextmenu';
import { TableModule } from 'primeng/table';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { CheckboxModule } from 'primeng/checkbox';
import { ToolbarModule } from 'primeng/toolbar';
import { DropdownModule } from 'primeng/dropdown';
import { AccordionModule } from 'primeng/accordion';
import { MessageModule } from 'primeng/message';
import { RippleModule } from 'primeng/ripple';
import { MessagesModule } from 'primeng/messages';
import { TagModule } from 'primeng/tag';
import { TabMenuModule } from 'primeng/tabmenu';
import { MonitoringWidgetComponent } from './monitoring-widget/monitoring-widget.component';
import { MenuModule } from 'primeng/menu';
import { MenubarModule } from 'primeng/menubar';
import { ChartModule } from 'primeng/chart';
import { CardModule } from 'primeng/card';

import { MonitoringModule } from './monitoring/monitoring.module';
import { CalendarModule } from 'primeng/calendar';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { SettingsModule } from './settings/settings.module';
import { JobModule } from './job/job.module';
import { ConfirmationService } from 'primeng/api';
@NgModule({
  declarations: [
    MonitoringWidgetComponent,
    DashboardComponent,
    UnauthorizedComponent,
  ],
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    BrowserModule,
    ReactiveFormsModule,
    TreeTableModule,
    ButtonModule,
    DialogModule,
    MultiSelectModule,
    InputTextModule,
    ToastModule,
    ContextMenuModule,
    FormsModule,
    TableModule,
    AutoCompleteModule,
    CheckboxModule,
    ToolbarModule,
    DropdownModule,
    AccordionModule,
    BoardLayoutModule,
    DashboardRoutingModule,
    MessageModule,
    MessagesModule,
    RippleModule,
    TagModule,
    TabMenuModule,
    MenuModule,
    MenubarModule,
    SettingsModule,
    MenuModule,
    TableModule,
    CardModule,
    FileUploadModule,
    MonitoringModule,
    JobModule,
    ChartModule,
    CalendarModule,
    ConfirmDialogModule,
  ],
  providers : [ConfirmationService],
  exports: [DashboardComponent],
})
export class PagesModule {}
