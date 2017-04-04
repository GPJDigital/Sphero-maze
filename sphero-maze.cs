int UP = 0;
int DOWN = 180;
int LEFT = 270;
int RIGHT = 90;

int CURRENT_MOTION = 0;

int SPEED = 80;
float DELAY = 0.2;


int getCurrentMotionDegrees() {
    if (CURRENT_MOTION == 0) {
        setRgbLed(255,0,0);
        return UP;
    } else if (CURRENT_MOTION == 1) {
        setRgbLed(0,255,0);
        return LEFT;
    } else if (CURRENT_MOTION == 2) {
        setRgbLed(0,0,255);
        return DOWN;
    } else {
        setRgbLed(50,50,50);
        return RIGHT;
    }
}


void updateNextMotion() {
    if (CURRENT_MOTION == 3) {
        CURRENT_MOTION = 0;
    } else {
        CURRENT_MOTION = CURRENT_MOTION + 1;
    }
}


void turn() {
    controlSystemTargetYaw += getCurrentMotionDegrees();
    delay(DELAY);
    updateNextMotion();
}


void move() {
    controlSystemTargetSpeed = SPEED;
    delay(0.9);

    if (locatorVelocityX < 0.1 or accelSensorYForward < 0.1) {
        controlSystemTargetSpeed = 0;
        turn();
        delay(DELAY);
    }
}


void handleCollision() {
    turn();
}


onCollisionEvent = &handleCollision;


void startProgram() {
    while(1) {
        move();
        delay(DELAY);
    }
}
