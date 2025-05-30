import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'tsi-avatar',
  template: ` <div class="flex align-items-center">
                <img
                    style="border: solid #fdfdfd91 1px; border-radius: 50%;"
                    [width]="size == 'medium'? '32' : '24'"
                    [height]="size == 'medium'? '32' : '24'"
                    [src]="src"
                    [alt]="label"
                    
                />
                <div
                    [class]="'mr-2 p-1 cursor-pointer ' + styleClass"
                    [style]="style"
                >
                {{ label }}
                </div>
            </div>`,
})
export class AvatarComponent implements OnInit {

  @Input() label: string = '';

  @Input() src: string = '';

  @Input() styleClass!: string ;

  @Input() style: string | undefined;

  @Input() size : string = 'medium'

  constructor() {}

  ngOnInit(): void {}
}
