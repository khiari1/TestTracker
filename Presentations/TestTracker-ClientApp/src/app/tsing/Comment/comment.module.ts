import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { EditorModule } from 'primeng/editor';
import { InplaceModule } from 'primeng/inplace';
import { CommentComponent } from './comment.component';

@NgModule({
  declarations: [CommentComponent],
  imports: [EditorModule, FormsModule, InplaceModule,CommonModule],
  exports: [CommentComponent],
})
export class CommentModule {}
