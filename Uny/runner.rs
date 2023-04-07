mod program;

extern "C" {
    pub fn __debug_log(ptr: *const u8, len: i32);
}

mod uny {
    use crate::__debug_log;

    pub fn debug_log(s: &str) {
        unsafe {
            __debug_log(s.as_ptr(), s.len() as i32);
        }
    }
}

#[no_mangle]
pub extern fn run() {
    program::main();
}
