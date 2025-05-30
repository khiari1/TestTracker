import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SideBarMenuComponent } from './side-bar-menu/side-bar-menu.component';
import { DashboardLayoutComponent } from './dashboard-layout.component';
import { HeaderMenuComponent } from './header-menu/header-menu.component';

import {PanelMenuModule} from 'primeng/panelmenu';
import { MenuModule } from 'primeng/menu';
import { TooltipModule } from 'primeng/tooltip';
import { ButtonModule } from 'primeng/button';
import { OverlayPanelModule } from 'primeng/overlaypanel';
import { AvatarModule } from 'src/app/tsing/avatar/avatar.module';
import { NotificationModule } from 'src/app/tsing/notification/notification.module';
import { MenuNavBarModule } from 'src/app/tsing/menu-nav-bar/menu-nav-bar.module';
import { BreadcrumbModule } from 'src/app/tsing/breadcrumb/breadcrumb.module';
import { RouterModule } from '@angular/router';
import { MenuBarModule } from "../../tsing/menu-bar/menu-bar.module";

@NgModule({
  declarations: [
    SideBarMenuComponent,
    DashboardLayoutComponent,
    HeaderMenuComponent,
 
  ],
  imports: [
    CommonModule,
    MenuModule,
    TooltipModule,
    ButtonModule,
    OverlayPanelModule,
    AvatarModule,
    PanelMenuModule,
    NotificationModule,
    MenuNavBarModule,
    BreadcrumbModule,
    RouterModule,
    MenuBarModule
],
  exports : [
    DashboardLayoutComponent
  ],
})
export class BoardLayoutModule { }
