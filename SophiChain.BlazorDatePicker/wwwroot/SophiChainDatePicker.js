// SophiChain Blazor DatePicker - Combined JavaScript
// Global functions for DOM operations and scroll management

class SCBJsApi {
    updateStyleProperty(elementId, property, value) {
        const element = document.getElementById(elementId);
        if (element) {
            element.style.setProperty(property, value);
        }
    }

    getBoundingClientRect(elementId) {
        const element = document.getElementById(elementId);
        if (element) {
            const rect = element.getBoundingClientRect();
            return {
                x: rect.x,
                y: rect.y,
                width: rect.width,
                height: rect.height,
                top: rect.top,
                right: rect.right,
                bottom: rect.bottom,
                left: rect.left
            };
        }
        return null;
    }

    addClass(elementId, className) {
        const element = document.getElementById(elementId);
        if (element) {
            element.classList.add(className);
        }
    }

    removeClass(elementId, className) {
        const element = document.getElementById(elementId);
        if (element) {
            element.classList.remove(className);
        }
    }

    toggleClass(elementId, className) {
        const element = document.getElementById(elementId);
        if (element) {
            element.classList.toggle(className);
        }
    }

    hasClass(elementId, className) {
        const element = document.getElementById(elementId);
        return element ? element.classList.contains(className) : false;
    }

    setVisible(elementId, visible) {
        const element = document.getElementById(elementId);
        if (element) {
            element.style.display = visible ? '' : 'none';
        }
    }
}

class SCBScrollManager {
    scrollToYear(elementId) {
        const element = document.getElementById(elementId);
        if (element) {
            element.parentNode.scrollTop = element.offsetTop - element.parentNode.offsetTop - element.scrollHeight * 3;
        }
    }

    scrollToElement(elementId) {
        const element = document.getElementById(elementId);
        if (element) {
            element.scrollIntoView({ 
                behavior: 'smooth', 
                block: 'center', 
                inline: 'start' 
            });
        }
    }

    scrollToListItem(elementId) {
        const element = document.getElementById(elementId);
        if (element) {
            const parent = element.parentElement;
            if (parent) {
                parent.scrollTop = element.offsetTop;
            }
        }
    }

    scrollTo(selector, left, top, behavior) {
        const element = document.querySelector(selector) || document.documentElement;
        element.scrollTo({ left, top, behavior });
    }

    scrollIntoView(selector, behavior) {
        const element = document.querySelector(selector) || document.documentElement;
        if (element) {
            element.scrollIntoView({ behavior, block: 'center', inline: 'start' });
        }
    }
}

class SCBClickOutside {
    addListener(elementId, dotNetRef) {
        const element = document.getElementById(elementId);
        if (element) {
            const clickHandler = (event) => {
                if (!element.contains(event.target)) {
                    dotNetRef.invokeMethodAsync('OnClickOutside');
                }
            };
            
            // Store the handler on the element for cleanup
            element._scbClickHandler = clickHandler;
            document.addEventListener('click', clickHandler);
        }
    }

    removeListener(elementId) {
        const element = document.getElementById(elementId);
        if (element && element._scbClickHandler) {
            document.removeEventListener('click', element._scbClickHandler);
            delete element._scbClickHandler;
        }
    }
}

class SCBDatePicker {
    scrollToYear(year) {
        // Add a small delay to ensure DOM is fully rendered
        setTimeout(() => {
            const yearElement = document.getElementById(`year-${year}`);
            if (yearElement) {
                const scrollContainer = yearElement.closest('.scb-year-scroll');
                if (scrollContainer) {
                    // Use scrollIntoView for more reliable centering
                    yearElement.scrollIntoView({
                        behavior: 'smooth',
                        block: 'center',
                        inline: 'nearest'
                    });
                }
            }
        }, 100);
    }

    scrollToMonth(month) {
        // Add a small delay to ensure DOM is fully rendered
        setTimeout(() => {
            const monthElement = document.getElementById(`month-${month}`);
            if (monthElement) {
                const scrollContainer = monthElement.closest('.scb-month-scroll');
                if (scrollContainer) {
                    // Use scrollIntoView for more reliable centering
                    monthElement.scrollIntoView({
                        behavior: 'smooth',
                        block: 'center',
                        inline: 'nearest'
                    });
                }
            }
        }, 100);
    }
}

// Attach to window object for global access
window.scbJsApi = new SCBJsApi();
window.scbScrollManager = new SCBScrollManager();
window.scbClickOutside = new SCBClickOutside();
window.scbDatePicker = new SCBDatePicker();
