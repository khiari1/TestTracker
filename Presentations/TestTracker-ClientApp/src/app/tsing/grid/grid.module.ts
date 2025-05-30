import { NgModule } from "@angular/core";
import { GridComponent } from "./grid.component";
import { BrowserModule } from "@angular/platform-browser";
import { InputTextModule } from "primeng/inputtext";
import { ButtonModule } from "primeng/button";
import { RippleModule } from "primeng/ripple";
import { TableModule } from "primeng/table";
import { TagModule } from "primeng/tag";
import { ColumnDirective, ColumnsDirective } from "./columns.directive";
import { CommonModule } from "@angular/common";

@NgModule({
  imports : [
    BrowserModule,
    CommonModule,
    InputTextModule,
    ButtonModule,
    RippleModule,
    TableModule,
    TagModule
  ],
   declarations : [
      GridComponent,
      ColumnDirective,
      ColumnsDirective
   ],
   exports : [GridComponent,ColumnDirective,
    ColumnsDirective]
})
export class GridModule{

}
