import { NgModule } from '@angular/core';
import { SpinnerComponent } from './spinner.component';
import { CommentModule } from '../Comment/comment.module';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [SpinnerComponent],
  imports: [CommentModule,BrowserModule],
  exports: [SpinnerComponent],
})
export class SpinnerModule {}
