import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
    providedIn : 'root'
})
export class SideBarToggleService{
    public expanded : Subject<boolean> = new Subject<boolean>();

    public toggle(value: boolean){
        this.expanded?.next(value);
    }
}