import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PageHeaderComponent } from './page-header.component';
import { MenubarModule } from 'primeng/menubar';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { MenuBarModule } from "../menu-bar/menu-bar.module";

@NgModule({
  declarations: [PageHeaderComponent],
  imports: [CommonModule, MenubarModule, BreadcrumbModule, MenuBarModule],
  exports: [PageHeaderComponent],
})
export class PageHeaderbModule {}
