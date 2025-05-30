import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuNavBar } from './menu-nav-bar.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [MenuNavBar],
  imports: [CommonModule,RouterModule],
  providers: [],
  exports: [MenuNavBar],
})
export class MenuNavBarModule {}
