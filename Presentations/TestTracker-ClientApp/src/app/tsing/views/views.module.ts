import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { FilterFormView, ViewListComponent } from './view-list.component';
import { PageHeaderbModule } from '../page-header/pageHeader.module';
import { MessagesModule } from 'primeng/messages';
import { ChipModule } from 'primeng/chip';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { ColumnViewDirective } from './column-view.directive';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { MultiSelectModule } from 'primeng/multiselect';
import { DropdownModule } from 'primeng/dropdown';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';
import { CalendarModule } from 'primeng/calendar';
import { ModalFormViewComponent } from './modal-form-view.component';
import { FormViewComponent } from './form-view.component';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { SpinnerModule } from '../spinner/spinner.module';
import { SplitButtonModule } from 'primeng/splitbutton';
import { MenuBarModule } from "../menu-bar/menu-bar.module";

@NgModule({
  declarations: [ViewListComponent,ColumnViewDirective,ModalFormViewComponent,FormViewComponent,FilterFormView],
  imports: [
    CommonModule,
    BreadcrumbModule,
    PageHeaderbModule,
    MessagesModule,
    ChipModule,
    InputTextModule,
    TableModule,
    OverlayPanelModule,
    MultiSelectModule,
    DropdownModule,
    TranslateModule,
    ButtonModule,
    FormsModule,
    CalendarModule,
    ConfirmDialogModule,
    SpinnerModule,
    SplitButtonModule,
    MenuBarModule,
],
  providers:[TranslateService],
  exports: [ViewListComponent,ColumnViewDirective,ModalFormViewComponent,FormViewComponent],
})
export class ViewsModule {}
