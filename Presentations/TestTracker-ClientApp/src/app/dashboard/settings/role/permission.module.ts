import { NgModule } from '@angular/core';
import { PermissionComponent } from './permission.component';
import { MenubarModule } from 'primeng/menubar';
import { TabViewModule } from 'primeng/tabview';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { AddGroupComponent } from './add-group.component';
import { FormsModule } from '@angular/forms';
import { MessagesModule } from 'primeng/messages';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { EditGroupComponent } from './edit-group.component';
import { ChipModule } from 'primeng/chip';
import { InputSwitchModule } from 'primeng/inputswitch';
import { AddUserGroupComponent } from './add-user-group.compoent';
import { UpdateGroupPermissionComponent } from './update-permission-group.component';
import { CheckboxModule } from 'primeng/checkbox';
import { SharedModule } from '../../SharedModule.module';
import { PageHeaderbModule } from 'src/app/tsing/page-header/pageHeader.module';
import { AvatarModule } from 'src/app/tsing/avatar/avatar.module';

@NgModule({
  declarations: [
    PermissionComponent,
    AddGroupComponent,
    EditGroupComponent,
    AddUserGroupComponent,
    UpdateGroupPermissionComponent,
  ],
  imports: [
    MenubarModule,
    TabViewModule,
    TableModule,
    ButtonModule,
    FormsModule,
    MessagesModule,
    CommonModule,
    InputTextModule,
    ChipModule,
    InputSwitchModule,
    CheckboxModule,
    SharedModule,
    PageHeaderbModule,
    AvatarModule
  ],
})
export class PermissionModule {}
