import { Component, OnInit, ElementRef, Renderer, HostListener, HostBinding } from '@angular/core';

@Component({
  selector: 'app-parallax-background',
  templateUrl: './parallax-background.component.html',
  styleUrls: ['./parallax-background.component.scss']
})
export class ParallaxBackgroundComponent implements OnInit {

  /**
   * Parallax X
   */

  private x = 0;

  /**
   * Parallax Y
   */

  private y = 0;

  /**
   * Parallax Friction
   */

  private friction: number = 1 / 200;

  /**
   * Parallax translate background
   */

  @HostBinding('style.transform')
  transform = 'translate(' + this.x + 'px, ' + this.y + 'px) scale(1.1)';

  /**
   * Parallax effect
   * @param event Mouse Event
   */

  @HostListener('window:mousemove', ['$event'])
  private onMouseMove(event: MouseEvent): void {

    const lMouseX = Math.max(-100, Math.min(100, window.innerWidth / 2 - event.clientX));
    const lMouseY = Math.max(-100, Math.min(100, window.innerHeight / 2 - event.clientY));

    const lFollowX: number = (30 * lMouseX) / 100;
    const lFollowY: number = (30 * lMouseY) / 100;

    this.x += (lFollowX - this.x) * this.friction;
    this.y += (lFollowY - this.y) * this.friction;

    this.renderer.setElementStyle(this.elRef.nativeElement, 'transform', 'translate(' + this.x + 'px, ' + this.y + 'px) scale(1.1)');
  }

  constructor(private readonly elRef: ElementRef, private readonly renderer: Renderer) { }

  ngOnInit() {
    this.renderer.setElementStyle(this.elRef.nativeElement, 'background',
      'url("../../../assets/img/backgrounds/home.jpg") center center no-repeat');
  }

}
