import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { MessagesModule } from 'primeng/messages';
import { TableModule } from 'primeng/table';
import { CardModule } from 'primeng/card';
import { UserComponent } from './user/user.component';
import { DropdownModule } from 'primeng/dropdown';
import { ModuleComponent } from './module/module.component';
import { ModuleFormComponent } from './module/module-form.component';
import { ConfirmPopupModule } from 'primeng/confirmpopup';
import { AttachementModule } from 'src/app/tsing/attachement/attachement.module';
import { SettingsV2Component } from './settings-v2.component';
import { RouterModule } from '@angular/router';
import { MenuModule } from 'primeng/menu';
import { TabViewModule } from 'primeng/tabview';
import { SplitterModule } from 'primeng/splitter';
import { InputSwitchModule } from 'primeng/inputswitch';
import { CheckboxModule } from 'primeng/checkbox';
import { TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient} from '@angular/common/http';
import { PermissionModule } from './role/permission.module';
import { UserMeComponent } from './user/user.me.component';
import { ProgressBarModule } from 'primeng/progressbar';
import { FileUploadModule } from 'primeng/fileupload';
import { GeneralSettingsComponent } from './general-settings/generalSettings.component';
import { SpeedDialModule } from 'primeng/speeddial';
import { TeamSettingsComponent } from '../settings/team-setting.component';
import { UserEditComponent } from './user/user-form.component';
import { InviteUserComponent } from './user/invite-user.component';
import { MenubarModule } from 'primeng/menubar';
import { TagModule } from 'primeng/tag';
import { FieldsetModule } from 'primeng/fieldset';
import { SharedModule } from '../SharedModule.module';
import { PageHeaderbModule } from 'src/app/tsing/page-header/pageHeader.module';
import { ViewsModule } from 'src/app/tsing/views/views.module';
import { AvatarModule } from 'src/app/tsing/avatar/avatar.module';
import { PanelModule } from 'primeng/panel';
import { ProjectFileComponent } from './general-settings/project-file.component';
import { FeatureComponent } from './feature/feature.component';
import { FeatureFormComponent } from './feature/feature-form.component';
import { LabelFormComponent } from './Label/label-form.component';
import { LabelComponent } from './Label/label.component';
import { ColorPickerModule } from 'primeng/colorpicker';

export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    UserComponent,
    ModuleComponent,
    ModuleFormComponent,
    SettingsV2Component,
    UserMeComponent,
    GeneralSettingsComponent,
    TeamSettingsComponent,
    UserEditComponent,
    InviteUserComponent,
    ProjectFileComponent,
    FeatureComponent ,
    FeatureFormComponent,
    LabelFormComponent,
    LabelComponent
  ],
  imports: [
    TabViewModule,
    TableModule,
    InputTextModule,
    RouterModule,
    FormsModule,
    ButtonModule,
    MessageModule,
    MessagesModule,
    BrowserModule,
    CardModule,
    DropdownModule,
    ConfirmPopupModule,
    AttachementModule,
    MenuModule,
    SplitterModule,
    InputSwitchModule,
    CheckboxModule,
    PermissionModule,
    TranslateModule,
    ProgressBarModule,
    FileUploadModule,
    SpeedDialModule,
    MenubarModule,
    TagModule,
    FieldsetModule,
    [SharedModule],
    PageHeaderbModule,
    PanelModule,
    ViewsModule,
    AvatarModule,
    ColorPickerModule
  ],
  exports: [SettingsV2Component],
})
export class SettingsModule {}
