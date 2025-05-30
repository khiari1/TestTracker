import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { PermissionComponent } from "./permission.component";
import { EditGroupComponent } from "./edit-group.component";
import { PermissionGuard } from 'src/app/guard/auth-guard.service';

const routes: Routes = [
  {
    path: '',
    component: PermissionComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPermission: 'Group_Read',
    },
  },
  {
    path: 'group',
    component: EditGroupComponent,
    canActivate: [PermissionGuard],
    data: {
      requiredPermission: 'Group_ReadWrite',
    },
  },
];
@NgModule({
  imports : [RouterModule.forChild(routes)],
  exports : [RouterModule]
})
export class PermissionRoutingModule{}
