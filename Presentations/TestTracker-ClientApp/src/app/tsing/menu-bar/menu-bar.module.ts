import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MenuBar } from './menu-bar.component';
import { TooltipModule } from 'primeng/tooltip';

@NgModule({
  declarations: [MenuBar],
  imports: [CommonModule,RouterModule,TooltipModule],
  providers: [],
  exports: [MenuBar],
})
export class MenuBarModule {}
