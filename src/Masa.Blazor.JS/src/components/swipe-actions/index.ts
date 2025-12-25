import { useVelocity } from "../../mixins/touch";

export interface SwipeActionsOptions {
  /** 滑动阈值，超过此值才会打开/关闭 */
  threshold?: number;
  /** 动画持续时间 */
  duration?: number;
  /** 是否禁用滑动 */
  disabled?: boolean;
  /** 最小滑动距离，低于此值视为点击 */
  minSwipeDistance?: number;
}

export interface SwipeActionsState {
  /** 当前偏移量 */
  offset: number;
  /** 当前位置 */
  position: 'left' | 'right' | 'center';
  /** 是否正在拖拽 */
  dragging: boolean;
}

export function useTouch(wrapperElement: HTMLElement, options: SwipeActionsOptions = {}) {
  const {
    threshold = 0.5,
    duration = 300,
    disabled = false,
    minSwipeDistance = 10
  } = options;

  const velocity = useVelocity();

  let state: SwipeActionsState = {
    offset: 0,
    position: 'center',
    dragging: false
  };

  let startX = 0;
  let startOffset = 0;
  let touchId: number | null = null;
  let isMouseDown = false;
  let hasStartedSwipe = false;

  // 获取左右元素的宽度
  function getElementWidths() {
    const leftElement = wrapperElement.querySelector('.m-swipe-actions__left') as HTMLElement;
    const rightElement = wrapperElement.querySelector('.m-swipe-actions__right') as HTMLElement;

    const leftWidth = leftElement ? leftElement.offsetWidth : 0;
    const rightWidth = rightElement ? rightElement.offsetWidth : 0;

    return { leftWidth, rightWidth };
  }

  // 设置wrapper的transform和transition
  function setTransform(offset: number, withTransition = false) {
    const style = wrapperElement.style;
    style.transform = `translate3d(${offset}px, 0, 0)`;
    style.transitionDuration = withTransition ? `${duration}ms` : '0ms';

    state.offset = offset;
  }

  // 获取目标偏移量
  function getTargetOffset(currentOffset: number, velocityX: number): number {
    const { leftWidth, rightWidth } = getElementWidths();
    const absOffset = Math.abs(currentOffset);
    const direction = currentOffset > 0 ? 1 : -1;

    // 根据速度判断意图
    if (Math.abs(velocityX) > 500) {
      if (velocityX > 0 && leftWidth > 0) {
        return leftWidth;
      }
      if (velocityX < 0 && rightWidth > 0) {
        return -rightWidth;
      }
      return 0;
    }

    // 根据阈值判断
    if (direction > 0 && leftWidth > 0) {
      return absOffset > leftWidth * threshold ? leftWidth : 0;
    }

    if (direction < 0 && rightWidth > 0) {
      return absOffset > rightWidth * threshold ? -rightWidth : 0;
    }

    return 0;
  }

  // 更新位置状态
  function updatePosition(offset: number) {
    let newPosition: 'left' | 'right' | 'center' = 'center';

    if (offset > 0) {
      newPosition = 'left';
    } else if (offset < 0) {
      newPosition = 'right';
    }

    if (state.position !== newPosition) {
      state.position = newPosition;
    }
  }

  // 触摸开始
  function onTouchStart(event: TouchEvent) {
    if (disabled || event.touches.length !== 1) return;

    const touch = event.touches[0];
    touchId = touch.identifier;
    startX = touch.clientX;
    startOffset = state.offset;
    state.dragging = false;  // 初始不设为 true，等待移动确认
    hasStartedSwipe = false;

    velocity.addMovement(event);
  }

  // 触摸移动
  function onTouchMove(event: TouchEvent) {
    if (disabled || touchId === null) return;

    velocity.addMovement(event);

    const touch = Array.from(event.touches).find(t => t.identifier === touchId);
    if (!touch) return;

    const deltaX = touch.clientX - startX;

    // 如果移动距离小于阈值，不开始滑动
    if (Math.abs(deltaX) < minSwipeDistance) {
      return;
    }

    // 开始滑动
    if (!hasStartedSwipe) {
      hasStartedSwipe = true;
      state.dragging = true;
      event.preventDefault();  // 现在阻止默认行为
    }

    let newOffset = startOffset + deltaX;
    const { leftWidth, rightWidth } = getElementWidths();

    // 限制滑动范围
    if (newOffset > 0) {
      // 向右滑动，显示左侧区域
      if (leftWidth <= 0) {
        newOffset = 0;
      } else {
        newOffset = Math.min(newOffset, leftWidth);
      }
    } else {
      // 向左滑动，显示右侧区域
      if (rightWidth <= 0) {
        newOffset = 0;
      } else {
        newOffset = Math.max(newOffset, -rightWidth);
      }
    }

    setTransform(newOffset, false);
    event.preventDefault();
  }

  // 触摸结束
  function onTouchEnd(event: TouchEvent) {
    if (disabled || touchId === null) return;

    state.dragging = false;
    hasStartedSwipe = false;

    try {
      const velocityData = velocity.getVelocity(touchId);
      const targetOffset = getTargetOffset(state.offset, velocityData.x);

      setTransform(targetOffset, true);
      updatePosition(targetOffset);
    } catch (error) {
      const targetOffset = getTargetOffset(state.offset, 0);
      setTransform(targetOffset, true);
      updatePosition(targetOffset);
    }

    velocity.endTouch(event);
    touchId = null;
  }

  // 触摸取消
  function onTouchCancel(event: TouchEvent) {
    if (disabled) return;

    state.dragging = false;
    hasStartedSwipe = false;
    velocity.endTouch(event);
    touchId = null;

    // 恢复到最近的稳定位置
    const targetOffset = getTargetOffset(state.offset, 0);
    setTransform(targetOffset, true);
    updatePosition(targetOffset);
  }

  // 鼠标按下
  function onMouseDown(event: MouseEvent) {
    if (disabled || event.button !== 0 || isMouseDown) return; // 只处理左键

    isMouseDown = true;
    startX = event.clientX;
    startOffset = state.offset;
    state.dragging = true;

    // 为鼠标事件创建一个模拟的touch事件用于速度计算
    const mockTouch = {
      identifier: 0,
      clientX: event.clientX,
      clientY: event.clientY,
      pageX: event.pageX,
      pageY: event.pageY,
      target: event.target,
      radiusX: 0,
      radiusY: 0,
      rotationAngle: 0,
      force: 1
    };

    const mockTouchEvent = {
      timeStamp: Date.now(),
      touches: [mockTouch],
      changedTouches: [mockTouch],
      targetTouches: [mockTouch],
      preventDefault: () => {},
      stopPropagation: () => {}
    } as any;

    velocity.addMovement(mockTouchEvent);

    // 阻止默认行为，防止选中文本
    event.preventDefault();
  }

  // 鼠标移动
  function onMouseMove(event: MouseEvent) {
    if (disabled || !state.dragging || !isMouseDown) return;

    // 为鼠标事件创建一个模拟的touch事件用于速度计算
    const mockTouch = {
      identifier: 0,
      clientX: event.clientX,
      clientY: event.clientY,
      pageX: event.pageX,
      pageY: event.pageY,
      target: event.target,
      radiusX: 0,
      radiusY: 0,
      rotationAngle: 0,
      force: 1
    };

    const mockTouchEvent = {
      timeStamp: Date.now(),
      touches: [mockTouch],
      changedTouches: [mockTouch],
      targetTouches: [mockTouch],
      preventDefault: () => {},
      stopPropagation: () => {}
    } as any;

    velocity.addMovement(mockTouchEvent);

    const deltaX = event.clientX - startX;
    let newOffset = startOffset + deltaX;
    const { leftWidth, rightWidth } = getElementWidths();

    // 限制滑动范围
    if (newOffset > 0) {
      // 向右滑动，显示左侧区域
      if (leftWidth <= 0) {
        newOffset = 0;
      } else {
        newOffset = Math.min(newOffset, leftWidth);
      }
    } else {
      // 向左滑动，显示右侧区域
      if (rightWidth <= 0) {
        newOffset = 0;
      } else {
        newOffset = Math.max(newOffset, -rightWidth);
      }
    }

    setTransform(newOffset, false);
    event.preventDefault();
  }

  // 鼠标释放
  function onMouseUp(event: MouseEvent) {
    if (disabled || !state.dragging || !isMouseDown) return;

    state.dragging = false;
    isMouseDown = false;

    try {
      const velocityData = velocity.getVelocity(0); // 使用identifier 0
      const targetOffset = getTargetOffset(state.offset, velocityData.x);

      setTransform(targetOffset, true);
      updatePosition(targetOffset);
    } catch (error) {
      // 如果获取速度失败，使用当前偏移量判断
      const targetOffset = getTargetOffset(state.offset, 0);
      setTransform(targetOffset, true);
      updatePosition(targetOffset);
    }

    // 为鼠标事件创建一个模拟的touch事件用于结束速度计算
    const mockTouch = {
      identifier: 0,
      clientX: event.clientX,
      clientY: event.clientY,
      pageX: event.pageX,
      pageY: event.pageY,
      target: event.target,
      radiusX: 0,
      radiusY: 0,
      rotationAngle: 0,
      force: 1
    };

    const mockTouchEvent = {
      timeStamp: Date.now(),
      touches: [],
      changedTouches: [mockTouch],
      targetTouches: [],
      preventDefault: () => {},
      stopPropagation: () => {}
    } as any;

    velocity.endTouch(mockTouchEvent);
  }

  // 鼠标离开
  function onMouseLeave(event: MouseEvent) {
    if (disabled || !state.dragging || !isMouseDown) return;

    state.dragging = false;
    isMouseDown = false;

    // 为鼠标事件创建一个模拟的touch事件用于结束速度计算
    const mockTouch = {
      identifier: 0,
      clientX: event.clientX,
      clientY: event.clientY,
      pageX: event.pageX,
      pageY: event.pageY,
      target: event.target,
      radiusX: 0,
      radiusY: 0,
      rotationAngle: 0,
      force: 1
    };

    const mockTouchEvent = {
      timeStamp: Date.now(),
      touches: [],
      changedTouches: [mockTouch],
      targetTouches: [],
      preventDefault: () => {},
      stopPropagation: () => {}
    } as any;

    velocity.endTouch(mockTouchEvent);

    // 恢复到最近的稳定位置
    const targetOffset = getTargetOffset(state.offset, 0);
    setTransform(targetOffset, true);
    updatePosition(targetOffset);
  }

  // 绑定事件
  function bindEvents() {
    // 触摸事件
    wrapperElement.addEventListener('touchstart', onTouchStart, { passive: false });
    wrapperElement.addEventListener('touchmove', onTouchMove, { passive: false });
    wrapperElement.addEventListener('touchend', onTouchEnd);
    wrapperElement.addEventListener('touchcancel', onTouchCancel);

    // 鼠标事件
    wrapperElement.addEventListener('mousedown', onMouseDown);
    // 鼠标移动和释放事件需要绑定到document，以便在鼠标移出元素时仍能捕获
    document.addEventListener('mousemove', onMouseMove);
    document.addEventListener('mouseup', onMouseUp);
    wrapperElement.addEventListener('mouseleave', onMouseLeave);
  }

  // 解绑事件
  function unbindEvents() {
    // 触摸事件
    wrapperElement.removeEventListener('touchstart', onTouchStart);
    wrapperElement.removeEventListener('touchmove', onTouchMove);
    wrapperElement.removeEventListener('touchend', onTouchEnd);
    wrapperElement.removeEventListener('touchcancel', onTouchCancel);

    // 鼠标事件
    wrapperElement.removeEventListener('mousedown', onMouseDown);
    document.removeEventListener('mousemove', onMouseMove);
    document.removeEventListener('mouseup', onMouseUp);
    wrapperElement.removeEventListener('mouseleave', onMouseLeave);
  }

  // 程序化控制方法
  function open(position: 'left' | 'right') {
    if (disabled) return;

    const { leftWidth, rightWidth } = getElementWidths();
    let targetOffset = 0;
    if (position === 'left' && leftWidth > 0) {
      targetOffset = leftWidth;
    } else if (position === 'right' && rightWidth > 0) {
      targetOffset = -rightWidth;
    }

    setTransform(targetOffset, true);
    updatePosition(targetOffset);
  }

  function close() {
    if (disabled) return;

    setTransform(0, true);
    updatePosition(0);
  }

  // 获取当前状态
  function getState(): SwipeActionsState {
    return { ...state };
  }

  return {
    bindEvents,
    unbindEvents,
    open,
    close,
    getState,
    setTransform
  };
}
