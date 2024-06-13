export class BaseProxy<T> {
  protected target: T;

  constructor(target: T) {
    this.target = target;
  }

  invokeVoid(prop: string, ...args: any[]) {
    if (this.target[prop] && typeof this.target[prop] === "function") {
      this.target[prop](...args);
    }
  }
}
