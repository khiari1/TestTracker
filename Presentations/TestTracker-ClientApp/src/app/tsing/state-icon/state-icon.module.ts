import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StateIconComponent } from './state-icon.component';
import { TagModule } from 'primeng/tag';



@NgModule({
  declarations: [
    StateIconComponent
  ],
  imports: [
    CommonModule,
    TagModule
  ],
  exports :[StateIconComponent]
})
export class StateIconModule { }
