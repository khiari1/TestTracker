import { NgModule } from "@angular/core";
import { BreadcrumbMenu } from "./breadcrumb.component";
import { RouterModule } from "@angular/router";
import { CommonModule } from "@angular/common";

@NgModule({
    imports : [CommonModule,RouterModule],
    declarations : [BreadcrumbMenu],
    exports : [BreadcrumbMenu]
})
export class BreadcrumbModule{

}