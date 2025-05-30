import { Directive, ElementRef, Input, OnInit, Renderer2 } from '@angular/core';
import { UserService } from '../services/user.service';



@Directive({
  selector: '[hasPermission]',
})
export class HasPermissionDirective implements OnInit {
  @Input() hasPermission!: string ;

  constructor(private elementRef: ElementRef,
    private renderer: Renderer2,
    private userService:UserService) {}

  ngOnInit(): void {

    if (!(this.userService.hasPermission(this.hasPermission) || this.userService.IsAdmin())) {
      this.renderer.setAttribute(
        this.elementRef.nativeElement,
        'disabled',
        'true'
      );
    }
  }


}
