import { NgModule } from "@angular/core";
import { Router, RouterModule, Routes } from "@angular/router";
import { ModuleComponent } from "./module/module.component";
import { UserComponent } from "./user/user.component";
import { UserMeComponent } from "./user/user.me.component";
import { GeneralSettingsComponent } from "./general-settings/generalSettings.component";
import { UserEditComponent } from "./user/user-form.component";
import { PermissionGuard } from 'src/app/guard/auth-guard.service';
import { UnauthorizedComponent } from "../unauthorized/unauthorized.component";
import { FeatureComponent } from "./feature/feature.component";
import { LabelComponent } from "./Label/label.component";

const routes: Routes = [
  {
    path: '',
    redirectTo: 'generalsettings',
    pathMatch: 'full',

  },

  {
    title: 'General Settings',
    path: 'generalsettings',
    component: GeneralSettingsComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPermission: 'Settings_Read',
    },
  },
  {
    path: 'users',
    title: 'Users',
    component: UserComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPermission: 'User_Read',
    },
  },
  {
    path: 'me',
    component: UserMeComponent,
    title: 'Me',
    canActivate: [PermissionGuard],
    data: {
      requiredPermission: 'User_Read',
    },
  },
  {
    path: 'users/:id',
    component: UserEditComponent,
    title: 'new user',
    canActivate: [PermissionGuard],
    data: {
      requiredPermission: 'User_ReadWrite',
    },
  },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent,
    title: 'Unauthorized',
  },    
  {
    path: 'modules',
    title: 'Modules',
    component: ModuleComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPermission: 'Module_Read',
    },
  },
  {
    path: 'features',
    component: FeatureComponent,
    title: 'Features',
    canActivate: [PermissionGuard],
    data: {
      requiredPermission: 'Feature_Read',
    },
  },
  {
    path: 'permissions',
    loadChildren: () =>
      import('./role/permission-routing.module').then(
        (m) => m.PermissionRoutingModule
      )
  },
  {
    path: 'labels',
    component: LabelComponent,
    title: 'Labels',
    canActivate: [PermissionGuard],
    data: {
      requiredPermission: 'Label_Read',
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class SettingsRoutingModule{

}
