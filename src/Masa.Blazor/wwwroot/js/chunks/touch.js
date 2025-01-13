import { _ as __classPrivateFieldGet, a as __classPrivateFieldSet } from './tslib.es6.js';

// The code is derived from Vuetify
// License: MIT
// Location: https://github.com/vuetifyjs/vuetify/blob/master/packages/vuetify/src/composables/touch.ts
var _CircularBuffer_arr, _CircularBuffer_pointer;
class CircularBuffer {
    constructor(size) {
        this.size = size;
        _CircularBuffer_arr.set(this, []);
        _CircularBuffer_pointer.set(this, 0);
    }
    push(val) {
        __classPrivateFieldGet(this, _CircularBuffer_arr, "f")[__classPrivateFieldGet(this, _CircularBuffer_pointer, "f")] = val;
        __classPrivateFieldSet(this, _CircularBuffer_pointer, (__classPrivateFieldGet(this, _CircularBuffer_pointer, "f") + 1) % this.size, "f");
    }
    values() {
        return __classPrivateFieldGet(this, _CircularBuffer_arr, "f").slice(__classPrivateFieldGet(this, _CircularBuffer_pointer, "f")).concat(__classPrivateFieldGet(this, _CircularBuffer_arr, "f").slice(0, __classPrivateFieldGet(this, _CircularBuffer_pointer, "f")));
    }
}
_CircularBuffer_arr = new WeakMap(), _CircularBuffer_pointer = new WeakMap();
const HORIZON = 100; // ms
const HISTORY = 20; // number of samples to keep
/** @see https://android.googlesource.com/platform/frameworks/native/+/master/libs/input/VelocityTracker.cpp */
function kineticEnergyToVelocity(work) {
    const sqrt2 = 1.41421356237;
    return (work < 0 ? -1.0 : 1.0) * Math.sqrt(Math.abs(work)) * sqrt2;
}
/**
 * Returns pointer velocity in px/s
 */
function calculateImpulseVelocity(samples) {
    // The input should be in reversed time order (most recent sample at index i=0)
    if (samples.length < 2) {
        // if 0 or 1 points, velocity is zero
        return 0;
    }
    // if (samples[1].t > samples[0].t) {
    //   // Algorithm will still work, but not perfectly
    //   consoleWarn('Samples provided to calculateImpulseVelocity in the wrong order')
    // }
    if (samples.length === 2) {
        // if 2 points, basic linear calculation
        if (samples[1].t === samples[0].t) {
            // consoleWarn(`Events have identical time stamps t=${samples[0].t}, setting velocity = 0`)
            return 0;
        }
        return (samples[1].d - samples[0].d) / (samples[1].t - samples[0].t);
    }
    // Guaranteed to have at least 3 points here
    // start with the oldest sample and go forward in time
    let work = 0;
    for (let i = samples.length - 1; i > 0; i--) {
        if (samples[i].t === samples[i - 1].t) {
            // consoleWarn(`Events have identical time stamps t=${samples[i].t}, skipping sample`)
            continue;
        }
        const vprev = kineticEnergyToVelocity(work); // v[i-1]
        const vcurr = (samples[i].d - samples[i - 1].d) / (samples[i].t - samples[i - 1].t); // v[i]
        work += (vcurr - vprev) * Math.abs(vcurr);
        if (i === samples.length - 1) {
            work *= 0.5;
        }
    }
    return kineticEnergyToVelocity(work) * 1000;
}
function useVelocity() {
    const touches = {};
    function addMovement(e) {
        Array.from(e.changedTouches).forEach(touch => {
            var _a;
            const samples = (_a = touches[touch.identifier]) !== null && _a !== void 0 ? _a : (touches[touch.identifier] = new CircularBuffer(HISTORY));
            samples.push([e.timeStamp, touch]);
        });
    }
    function endTouch(e) {
        Array.from(e.changedTouches).forEach(touch => {
            delete touches[touch.identifier];
        });
    }
    function getVelocity(id) {
        var _a;
        const samples = (_a = touches[id]) === null || _a === void 0 ? void 0 : _a.values().reverse();
        if (!samples) {
            throw new Error(`No samples for touch id ${id}`);
        }
        const newest = samples[0];
        const x = [];
        const y = [];
        for (const val of samples) {
            if (newest[0] - val[0] > HORIZON)
                break;
            x.push({ t: val[0], d: val[1].clientX });
            y.push({ t: val[0], d: val[1].clientY });
        }
        return {
            x: calculateImpulseVelocity(x),
            y: calculateImpulseVelocity(y),
            get direction() {
                const { x, y } = this;
                const [absX, absY] = [Math.abs(x), Math.abs(y)];
                return absX > absY && x >= 0 ? 'right'
                    : absX > absY && x <= 0 ? 'left'
                        : absY > absX && y >= 0 ? 'down'
                            : absY > absX && y <= 0 ? 'up'
                                : oops();
            },
        };
    }
    return { addMovement, endTouch, getVelocity };
}
function oops() {
    throw new Error();
}

export { useVelocity as u };
//# sourceMappingURL=touch.js.map
