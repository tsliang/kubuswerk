
// Constant definitions
const int RED_PIN    = 5;
const int GRN_PIN    = 6;
const int RED_ON     = 150;
const int GRN_ON     = 200;
const int MODE_OFF   = 0;
const int MODE_ON    = 1;
const int MODE_BLINK = 2;
const int MODE_PULSE = 3;

int grnMode  = 0;
int redMode  = 0;
int grnReps  = 0;
int redReps  = 0;
int redState = 0;
int grnState = 0;

void setup() {
  pinMode(RED_PIN, OUTPUT);
  pinMode(GRN_PIN, OUTPUT);
  Serial.begin(57600);
}

void loop() {
  while (Serial.available() > 0) {
    parseOpCode(Serial.read());
  }
  int i;
  runOn();
  runOff();
  for (i=0;i<100;i++) {
    runBlink();
    runPulse();
    delay(10);
  }
  checkReps();
}

void runOn() {
  if (grnMode == MODE_ON) {
    analogWrite(GRN_PIN, GRN_ON);
  }
  if (redMode == MODE_ON) {
    analogWrite(RED_PIN, RED_ON);
  }
}

void runOff() {
  if (grnMode == MODE_OFF) {
    analogWrite(GRN_PIN, 0);
  }
  if (redMode == MODE_OFF) {
    analogWrite(RED_PIN, 0);
  }
}

int redBlinkState = 0;
int grnBlinkState = 0;

void runBlink() {
  if (grnMode == MODE_BLINK) {
    if (grnBlinkState++ < 50) {
      analogWrite(GRN_PIN, GRN_ON);
    } else {
      analogWrite(GRN_PIN, 0);
    }
    grnBlinkState %= 100;
  }
  if (redMode == MODE_BLINK) {
    if (redBlinkState++ < 50) {
      analogWrite(RED_PIN, GRN_ON);
    } else {
      analogWrite(RED_PIN, 0);
    }
    redBlinkState %= 100;
  }
}

int redPulseState = 0;
int grnPulseState = 0;
int pulseStep = 0;
void runPulse() {
  if (redMode == MODE_PULSE) {
    _runPulse(&redPulseState, RED_PIN, RED_ON);
  }
  if (grnMode == MODE_PULSE) {
    _runPulse(&grnPulseState, GRN_PIN, GRN_ON);
  }  
  pulseStep = ++pulseStep % 100;
}

void _runPulse(int* pulseStateVar, int pin, int maxPower) {
  int pulseState = *pulseStateVar;
  if (pulseStep < 50) {
    pulseState++;
  } else {
    pulseState--;
  }
  int power = map(pulseState, 0, 100, 0, maxPower);
  analogWrite(pin, power);
  *pulseStateVar = pulseState;
}

int OP_RED   = 0b10000000;
int OP_GRN   = 0b01000000;
int OP_MODE  = 0b00110000;
int OP_REPS  = 0b00001111;
void parseOpCode(int opCode) {
  int mode = (opCode & OP_MODE) >> 4;
  int reps = (opCode & OP_REPS);
  if (opCode & OP_RED) {
    redMode = mode;
    redReps = reps;
    if (reps == 0) {
      redState = mode;
    }
  }
  if (opCode & OP_GRN) {
    grnMode = mode;
    grnReps = reps;
    if (reps == 0) {
      grnState = mode;
    }
  }
}

void checkReps() {
  _checkReps(&grnReps, &grnMode, &grnState);
  _checkReps(&redReps, &redMode, &redState);
}

void _checkReps(int* repVar, int* modeVar, int* stateVar) {
  int reps = *repVar;
  if (reps != 0) {
    reps--;
    if (reps == 0) {
      *modeVar = *stateVar;
    }
    *repVar = reps;
  }
}
