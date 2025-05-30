import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';

import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { AttachementComponent } from './attachement.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
  declarations: [AttachementComponent],

  imports: [
    BrowserModule,
    CommonModule,
    TableModule,
    ButtonModule,
    InputTextModule,
    FormsModule,
    TranslateModule
  
  ],
  exports: [AttachementComponent],
})
export class AttachementModule {}
